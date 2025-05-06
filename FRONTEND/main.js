let statsArray = []

async function getStatistics() {
    const response = await fetch('http://localhost:5229/text')
    const statistics = await response.json()
    console.log(statistics)

    const statTable = document.querySelector('#stat-table')

    const tr = document.createElement('tr')
    const charCount = document.createElement('td')
    const wordCount = document.createElement('td')
    const sentenceCount = document.createElement('td')
    const mostComWord = document.createElement('td')
    const readIndex = document.createElement('td')

    charCount.innerHTML = statistics.charCount
    wordCount.innerHTML = statistics.wordCount
    sentenceCount.innerHTML = statistics.sentenceCount
    mostComWord.innerHTML = statistics.mostComWord
    readIndex.innerHTML = statistics.readIndex

    charCount.classList.add('text-center')
    wordCount.classList.add('text-center')
    sentenceCount.classList.add('text-center')
    mostComWord.classList.add('text-center')
    readIndex.classList.add('text-center')

    tr.appendChild(charCount)
    tr.appendChild(wordCount)
    tr.appendChild(sentenceCount)
    tr.appendChild(mostComWord)
    tr.appendChild(readIndex)
    
    statTable.appendChild(tr)
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