async function getStatistics() {
    const response = await fetch('http://localhost:5229')
    const count = await response.json()
    console.log(count)
}
getStatistics()