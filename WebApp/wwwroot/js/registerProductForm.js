function Validation() {
    export const validateText = (elementName, value, minLength = 2) => {
        if (value.length == 0)
            return `${elementName} is required`
        else if (value.length < minLength)
            return `${elementName} must contain at least ${minLength} characters`
        else
            return ''
    }

    export const validateEmail = (elementName, value, regEx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/) => {
        if (value.length == 0)
            return `${elementName} is required`
        else if (!regEx.test(value))
            return `${elementName} must be a valid email address`
        else
            return ''
    }

    export const validatePassword = (elementName, value, minLength = 6) => {
        if (value.length === 0)
            return `${elementName} is required`
        if (value.length < minLength)
            return `${elementName} must contain at least ${minLength} characters`
        else
            return ''
    }

    export const handleChange = (e) => {
        const { id, value } = e.target
        setformData({ ...formData, [id]: value }) //vi tar ett id (name email comment) och sätter ett värde med values

        if (id === 'name')
            setErrors({ ...errors, [id]: validateText(id, value) })

        if (id === 'email')
            setErrors({ ...errors, [id]: validateEmail(id, value) })

        if (id === 'password')
            setErrors({ ...errors, [id]: validatePassword(id, value) })
    }

    export const handleTextAreaChange = (e) => {
        const { id, value } = e.target
        setformData({ ...formData, [id]: value })

        if (id === 'comments')
            setErrors({ ...errors, [id]: validateText(id, value, 3) })
    }
}
export default Validation

