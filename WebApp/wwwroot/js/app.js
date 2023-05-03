"use strict";
var __assign = (this && this.__assign) || function () {
    __assign = Object.assign || function(t) {
        for (var s, i = 1, n = arguments.length; i < n; i++) {
            s = arguments[i];
            for (var p in s) if (Object.prototype.hasOwnProperty.call(s, p))
                t[p] = s[p];
        }
        return t;
    };
    return __assign.apply(this, arguments);
};
Object.defineProperty(exports, "__esModule", { value: true });
exports.validateEmail = exports.validateText = void 0;
var react_1 = require("react");
var validateText = function (elementName, value, minLength) {
    if (minLength === void 0) { minLength = 2; }
    if (value.length == 0)
        return "".concat(elementName, " is required");
    else if (value.length < minLength)
        return "".concat(elementName, " must contain at least ").concat(minLength, " characters");
    else
        return '';
};
exports.validateText = validateText;
var validateEmail = function (elementName, value, regEx) {
    if (regEx === void 0) { regEx = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/; }
    if (value.length == 0)
        return "".concat(elementName, " is required");
    else if (!regEx.test(value))
        return "".concat(elementName, " must be a valid email address");
    else
        return '';
};
exports.validateEmail = validateEmail;
var validationSection = function () {
    var DEFAULT_VALUES = { name: '', email: '' };
    var _a = (0, react_1.useState)(DEFAULT_VALUES), formData = _a[0], setformData = _a[1];
    var _b = (0, react_1.useState)(DEFAULT_VALUES), errors = _b[0], setErrors = _b[1];
    var handleChange = function (e) {
        var _a, _b, _c;
        var _d = e.target, id = _d.id, value = _d.value;
        setformData(__assign(__assign({}, formData), (_a = {}, _a[id] = value, _a))); //vi tar ett id (name email comment) och sätter ett värde med values
        if (id === 'name')
            setErrors(__assign(__assign({}, errors), (_b = {}, _b[id] = (0, exports.validateText)(id, value), _b)));
        if (id === 'email')
            setErrors(__assign(__assign({}, errors), (_c = {}, _c[id] = (0, exports.validateEmail)(id, value), _c)));
    };
};
//# sourceMappingURL=app.js.map