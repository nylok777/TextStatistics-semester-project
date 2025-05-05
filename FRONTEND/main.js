async function getStatistics() {
    const response = await fetch('http://localhost:5229/text')
    const count = await response.json()
    console.log(count)
}

function sendData() {
    let txt = document.querySelector('#TxtInput').value
    console.log(txt)
    fetch('http://localhost:5229/text/', {
        method: 'POST',
        headers: {'Content-Type': 'application/json', },
        body: JSON.stringify(txt)
    }).then(resp => {
        console.log('Response: ', resp)
        if (resp.status === 200) {
            getStatistics()
        }
    }).catch(error => console.log(error))
}