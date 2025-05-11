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
    readIndex.innerHTML = Math.ceil(statistics.readIndex)

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
    const topWords = statistics.mostComWords

    drawWordCounts(topWordsCounts, topWords)
}

function drawWordCounts(topWordsCounts, topWords) {
    const chart = document.querySelector('#barchart')
    
    for (let i = 0; i < topWordsCounts.length; i++) {
        const element = topWordsCounts[i]
        const word = topWords[i]
        const mainDiv = document.getElementById('diagram')

        let longestWord = 0

        if (topWords[longestWord].length < word.length) {
            longestWord = i
        }

        let div = document.createElement('div')
        if (element > 19) {
            div.style.height = `${element*5}px`
        }
        else {
            div.style.height = `${(element*1.5)}cm`
        }
        
        div.style.backgroundColor = '#CBC3E3'
        div.classList.add('bar-divs', 'col-md-auto')
        div.innerHTML = `${element}`
        chart.appendChild(div)

        let pDiv = document.createElement('div')
        pDiv.innerHTML = word
        pDiv.classList.add('bar-divs-words', 'col-md-auto')
        document.querySelector('.bar-words-row').appendChild(pDiv)

        mainDiv.style.paddingBottom = `${topWords[longestWord].length*20}px`
    }
}

window.onload(getStatistics())