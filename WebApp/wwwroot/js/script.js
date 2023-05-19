/////////// FOOTER ///////////

function footerPosition(element, scrollHeight, innerHeight) { 
    try { 
        const _element = document.querySelector(element) //här försöker vi hitta vår footer
        const isTallerThanScreen = scrollHeight >= (innerHeight + _element.scrollHeight) //om sidans höjd (scrollheight) är större än sidan + footern

        _element.classList.toggle('position-fixed-bottom', !isTallerThanScreen)
        _element.classList.toggle('position-static', isTallerThanScreen)
    } catch { }
}
footerPosition('footer', document.body.scrollHeight, window.innerHeight)


/////////// TOGGLE BUTTON ///////////

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

/////////// USER CHOISE ///////////

function sendChoice(chioce) {

}


/////////// VALIDATION ///////////

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
validateText("#first-name-input")
validateText("#last-name-input")


function setError(element, errorMsg) {
    const parent = element.parentNode
    const errorElement = parent.querySelector("small span")
    errorElement.innerText = errorMsg
}


function validateEmail(selector) {
    try {
        const _element = document.querySelector(selector)
        _element.addEventListener('keyup', function () {

            const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/

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


function validatePassword(selector) {
    try {
        const _element = document.querySelector(selector)
        _element.addEventListener('keyup', function () {

            const passwordRegex = /^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^a-zA-Z0-9]).{8,}$/

            if (_element.value.length == 0)
                setError(_element, `${_element.name} is required`)
            else if (!passwordRegex.test(_element.value))
                setError(_element, `${_element.name} must be 8 characters and contain at least 1 capital letter, 1 lowcase letter, 1 number and 1 special character`)
            else
                setError(_element, '')
        })
    } catch { }
}
validatePassword("#password-input")


function validatePrice(selector) {
    try {
        const _element = document.querySelector(selector)
        _element.addEventListener('keyup', function () {

            const priceRegex = /^[0-9]+$/;

            if (_element.value.length == 0)
                setError(_element, `${_element.name} is required`)
            else if (!priceRegex.test(_element.value))
                setError(_element, `${_element.name} must contain only digits and at least one digit`)
            else
                setError(_element, '')
        })
    } catch { }
}
validatePrice("#price-input")


function validatePhone(selector) {
    try {
        const _element = document.querySelector(selector)
        _element.addEventListener('keyup', function () {

            const phoneRegex = /^(\+)?[0-9]{10,11}$/;

            if (_element.value.length == 0)
                setError(_element, `${_element.name} is required`)
            else if (!phoneRegex.test(_element.value))
                setError(_element, `${_element.name} must contain 10 digits or start with a '+' and contain 11 digits`)
            else
                setError(_element, '')
        })
    } catch { }
}
validatePhone("#phone-input")