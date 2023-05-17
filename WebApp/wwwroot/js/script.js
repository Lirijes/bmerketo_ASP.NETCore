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


function validateText(selector, minLength = 2) {
    try {
        const _element = document.querySelector(selector)
        _element.addEventListener('keyup', function () {

            if (_element.value.length == 0) 
                setError(_element, `${_element.name} is required`)
            else if (_element.value.length < minLength)
                setError(_element, `${_element.name} must contain at least ${minLength} characters`)
            else
                setError(_element, '')
        })
    } catch { }
}
validateText("#name-input")

function setError(element, errorMsg) {
    const parent = element.parentNode
    const errorElement = parent.querySelector("small span")
    errorElement.innerText = errorMsg
}

function validateEmail(selector) {
    try {
        const _element = document.querySelector(selector)
        _element.addEventListener('keyup', function () {

            const emailRegex = '/^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/'

            if (_element.value.length == 0)
                setError(_element, `${_element.name} is required`)
            else if (!emailRegex.test(_element.value))
                setError(_element, `${_element.name} must be a valid email address`)
            else
                setError(_element, '')
        })
    } catch { }
}
validateEmail("#email-input")


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