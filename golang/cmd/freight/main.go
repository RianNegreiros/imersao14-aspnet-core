package main

import (
	"database/sql"
	"encoding/json"
	"fmt"
	"net/http"

	"github.com/RianNegreiros/full-cycle-14/golang/internal/freight/entity"
	"github.com/RianNegreiros/full-cycle-14/golang/internal/freight/infra/repository"
	"github.com/RianNegreiros/full-cycle-14/golang/internal/freight/usecase"
	"github.com/RianNegreiros/full-cycle-14/golang/pkg/kafka"
	ckafka "github.com/confluentinc/confluent-kafka-go/kafka"
	"github.com/prometheus/client_golang/prometheus"
	"github.com/prometheus/client_golang/prometheus/promhttp"
)

var (
	routesCreated = prometheus.NewCounter(
		prometheus.CounterOpts{
			Name: "routes_created_total",
			Help: "Total number of created routes",
		},
	)

	routesStarted = prometheus.NewCounterVec(
		prometheus.CounterOpts{
			Name: "routes_started_total",
			Help: "Total number of started routes",
		},
		[]string{"status"},
	)

	errorsTotal = prometheus.NewCounter(
		prometheus.CounterOpts{
			Name: "errors_total",
			Help: "Total number of errors",
		},
	)
)

func init() {
	prometheus.MustRegister(routesStarted)
	prometheus.MustRegister(errorsTotal)
	prometheus.MustRegister(routesCreated)
}

func main() {
	msgChan := make(chan *ckafka.Message)
	topics := []string{"route"}
	servers := "host.docker.internal:9094"
	go kafka.Consume(topics, servers, msgChan)

	db, err := sql.Open("mysql", "root:root@tcp(host.docker.internal:3306)/routes?parseTime=true")
	if err != nil {
		panic(err)
	}

	defer func(db *sql.DB) {
		err := db.Close()
		if err != nil {

		}
	}(db)
	http.Handle("/metrics", promhttp.Handler())
	go func() {
		err := http.ListenAndServe(":8080", nil)
		if err != nil {

		}
	}()
	repositoryMySql := repository.NewRouteRepositoryMySql(db)
	freight := entity.NewFreight(10)
	createRouteUseCase := usecase.NewCreateRouteUseCase(repositoryMySql, freight)
	changeRouteStatusUseCase := usecase.NewChangeRouteStatusUseCase(repositoryMySql)

	for msg := range msgChan {
		input := usecase.CreateRouteInput{}
		err := json.Unmarshal(msg.Value, &input)
		if err != nil {
			return
		}

		switch input.Event {
		case "RouteCreated":
			_, err := createRouteUseCase.Execute(input)
			if err != nil {
				fmt.Println(err)
				errorsTotal.Inc()
			} else {
				routesCreated.Inc()
			}

		case "RouteStarted":
			input := usecase.ChangeRouteStatusInput{}
			err := json.Unmarshal(msg.Value, &input)
			if err != nil {
				return
			}
			_, err = changeRouteStatusUseCase.Execute(input)
			if err != nil {
				fmt.Println(err)
				errorsTotal.Inc()
			} else {
				routesStarted.WithLabelValues("started").Inc()
			}

		case "RouteFinished":
			input := usecase.ChangeRouteStatusInput{}
			err := json.Unmarshal(msg.Value, &input)
			if err != nil {
				return
			}
			_, err = changeRouteStatusUseCase.Execute(input)
			if err != nil {
				fmt.Println(err)
				errorsTotal.Inc()
			} else {
				routesStarted.WithLabelValues("finished").Inc()
			}
		}
	}
}
