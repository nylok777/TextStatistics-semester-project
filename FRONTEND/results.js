let headsArray = ['Karakterek száma', 'Szavak száma', 'Mondatok száma', 'Leggyakoribb szó', 'ARI olvashatósági index']
let headerCreated = false

async function getStatistics() {
    const response = await fetch('http://localhost:5229/text')
    const statistics = await response.json()
    console.log(statistics)
    
    const statBody = document.querySelector('#stat-body')

    const headRow = document.querySelector('#stat-head-row')
    if (headerCreated == false) {
        headsArray.forEach(x => {
            let th = document.createElement('th')
            th.innerHTML = x
            th.classList.add('text-center')
            headRow.appendChild(th)
        })
        headerCreated = true
    }
    

    const tr = document.createElement('tr')
    const charCount = document.createElement('td')
    const wordCount = document.createElement('td')
    const sentenceCount = document.createElement('td')
    const mostComWord = document.createElement('td')
    const readIndex = document.createElement('td')

    charCount.innerHTML = statistics.charCount
    wordCount.innerHTML = statistics.wordCount
    sentenceCount.innerHTML = statistics.sentenceCount
    mostComWord.innerHTML = statistics.mostComWords[0]
    readIndex.innerHTML = Math.round(statistics.readIndex)

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
    
    statBody.appendChild(tr)

    const topWordsCounts = statistics.mostComWordCounts

    drawWordCounts(topWordsCounts)
}

function drawWordCounts(topWordsCounts) {
    const chart = document.querySelector('.barchart')

    for (let i = 0; i < topWordsCounts.length; i++) {
        const element = topWordsCounts[i];
        let div = document.createElement('div')
        div.style.height = `${element}cm`
        div.classList.add('bar-divs')
        div.classList.add('col-md-auto')
        chart.appendChild(div)
    }
}

window.onload(getStatistics())