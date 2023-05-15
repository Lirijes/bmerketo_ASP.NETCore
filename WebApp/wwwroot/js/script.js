function footerPosition(element, scrollHeight, innerHeight) { 
    try { 
        const _element = document.querySelector(element) //här försöker vi hitta vår footer
        const isTallerThanScreen = scrollHeight >= (innerHeight + _element.scrollHeight) //om sidans höjd (scrollheight) är större än sidan + footern

        _element.classList.toggle('position-fixed-bottom', !isTallerThanScreen)
        _element.classList.toggle('position-static', isTallerThanScreen)
    } catch { }
}
footerPosition('footer', document.body.scrollHeight, window.innerHeight)


function toggleMenu(attribute) {
    try {
        const toggleBtn = document.querySelector(attribute)
        toggleBtn.addEventListener('click', function () {
            const element = document.querySelector(toggleBtn.getAttribute('data-target'))

            if (!element.classList.contains('open-menu')) {
                element.classList.add('open-menu')
                toggleBtn.classList.add('btn-outline-dark')
                toggleBtn.classList.add('btn-toggle-white')
            }

            else {
                element.classList.remove('open-menu')
                toggleBtn.classList.remove('btn-outline-dark')
                toggleBtn.classList.remove('btn-toggle-white')
            }
        })
    } catch { }
}
toggleMenu('[data-option="toggle"]')


function validateText(attribute, minLength = 2) {
    try {
        const _elementName = document.querySelector(attribute)
        _elementName.addEventListener('onkeypress', function () {
            const element = document.querySelector(_elementName.getElementById('text'))

            if (element.length == 0)
                return `${_elementName} is required`
            else if (element.length < minLength)
                return `${_elementName} must contain at least ${minLength} characters`
            else
                return ''
        })
    } catch { }
}
validateText("text")


//document.getElementById("text").onchange = function() {

//};


//function handleChange() {
//    const text = document.getElementById("text");
//    const textValue = text.onchange.length === 2;
//    text.onchange = textValue ? 2>0 : null;
//}

//const el = document.getElementById("divField");
//if (el) {
//    el.addEventListener("keypress", handleChange, false);
//}