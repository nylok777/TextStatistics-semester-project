window.onload = function() {
    document.getElementById('TxtInput').value = ''
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
            window.location.replace('results.html')
        }
    }).catch(error => console.log(error))
    document.getElementById('TxtInput').value = ''
}

function addText(input) {
    const expName = input.value
    fetch(`media/${expName}.txt`).then(x => x.text()).then(
        text => {
            document.getElementById('TxtInput').value = text            
        }
    )
}

function deleteText() {
    document.getElementById('TxtInput').value = ''
}