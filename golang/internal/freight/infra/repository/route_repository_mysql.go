package repository

import (
	"database/sql"
	"github.com/RianNegreiros/full-cycle-14/golang/internal/freight/entity"
)

type RouteRepositoryMySql struct {
	db *sql.DB
}

func NewRouteRepositoryMySql(db *sql.DB) *RouteRepositoryMySql {
	return &RouteRepositoryMySql{
		db: db,
	}
}

func (r *RouteRepositoryMySql) Create(route *entity.Route) error {
	query := "INSERT INTO routes (id, name, distance, status, freight_price) VALUES (?, ?, ?, ?,?)"
	_, err := r.db.Exec(query, route.ID, route.Name, route.Distance, route.Status, route.FreightPrice)
	if err != nil {
		return err
	}
	return nil
}

func (r *RouteRepositoryMySql) FindById(id string) (*entity.Route, error) {
	sqlStmt := "SELECT id, name, distance, status, freight_price, started_at, finished_at FROM routes WHERE id = ?"
	row := r.db.QueryRow(sqlStmt, id)

	var startedAt, finishedAt sql.NullTime

	var route entity.Route
	err := row.Scan(
		&route.ID,
		&route.Name,
		&route.Distance,
		&route.Status,
		&route.FreightPrice,
		&startedAt,
		&finishedAt,
	)

	if err != nil {
		return nil, err
	}

	if startedAt.Valid {
		route.FinishedAt = startedAt.Time
	}

	if finishedAt.Valid {
		route.FinishedAt = finishedAt.Time
	}

	return &route, nil
}

func (r *RouteRepositoryMySql) Update(route *entity.Route) error {
	startedAt := route.StartedAt.Format("2006-01-02 15:04:05")
	finishedAt := route.FinishedAt.Format("2006-01-02 15:04:05")
	query := "UPDATE routes SET status = ?, freight_price = ?, started_at=?, finished_at = ? WHERE id = ?"
	_, err := r.db.Exec(query, route.Status, route.FreightPrice, startedAt, finishedAt, route.ID)
	if err != nil {
		return err
	}
	return nil
}
