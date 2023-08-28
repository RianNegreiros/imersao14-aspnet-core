import Link from 'next/link'

export default function Home() {
  return (
    <div style={{ background: 'black', height: '100vh', display: 'flex', flexDirection: 'column', justifyContent: 'center', alignItems: 'center' }}>
      <Link href="/new-route" style={{ background: 'yellow', color: 'black', fontSize: '24px', padding: '10px 20px', border: 'none', borderRadius: '8px', marginBottom: '10px' }}>
        Crie uma nova rota
      </Link>
      <Link href="/driver" style={{ background: 'yellow', color: 'black', fontSize: '24px', padding: '10px 20px', border: 'none', borderRadius: '8px' }}>
        Veja suas rotas
      </Link>
    </div>
  )
}
