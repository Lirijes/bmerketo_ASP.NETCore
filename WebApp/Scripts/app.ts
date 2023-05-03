import React, { useState } from 'react'

export const validateText = (elementName: string, value: string, minLength: number = 2) => {
    if (value.length == 0)
        return `${elementName} is required`
    else if (value.length < minLength)
        return `${elementName} must contain at least ${minLength} characters`
    else
        return ''
}

export const validateEmail = (elementName: string, value: string, regEx: RegExp = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/) => {
    if (value.length == 0)
        return `${elementName} is required`
    else if (!regEx.test(value))
        return `${elementName} must be a valid email address`
    else
        return ''
}

export interface ContactUsType {
    name: string
    email: string
}

const validationSection: React.FC = () => {

    const DEFAULT_VALUES: ContactUsType = { name: '', email: '' }
    const [formData, setformData] = useState<ContactUsType>(DEFAULT_VALUES)
    const [errors, setErrors] = useState<ContactUsType>(DEFAULT_VALUES)

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { id, value } = e.target
        setformData({ ...formData, [id]: value }) //vi tar ett id (name email comment) och sätter ett värde med values

        if (id === 'name')
            setErrors({ ...errors, [id]: validateText(id, value) })

        if (id === 'email')
            setErrors({ ...errors, [id]: validateEmail(id, value) })
    }
}
