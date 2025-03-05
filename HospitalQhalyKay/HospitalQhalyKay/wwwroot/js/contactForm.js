document.addEventListener("DOMContentLoaded", () => {
    const form = document.getElementById("contactForm");

    const setErrors = (message, field, isError = true) => {
        const errorElement = field.nextElementSibling;
        if (isError) {
            field.classList.add("invalid");
            errorElement.classList.add("error");
            errorElement.innerText = message;
        } else {
            field.classList.remove("invalid");
            errorElement.classList.remove("error");
            errorElement.innerText = "";
        }
    };

    const validateEmptyField = (message, field) => {
        if (field.value.trim().length === 0) {
            setErrors(message, field);
            return false;
        } else {
            setErrors("", field, false);
            return true;
        }
    };

    const validateEmailFormat = (field) => {
        const regex = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
        if (field.value.trim().length === 0) {
            setErrors("Ingrese su dirección de correo", field);
            return false;
        } else if (!regex.test(field.value)) {
            setErrors("Ingrese un correo válido", field);
            return false;
        } else {
            setErrors("", field, false);
            return true;
        }
    };

    form.addEventListener("submit", (e) => {
        e.preventDefault();

        const firstName = form.querySelector("[name=firstName]");
        const lastName = form.querySelector("[name=lastName]");
        const email = form.querySelector("[name=email]");
        const subject = form.querySelector("[name=subject]");
        const message = form.querySelector("[name=message]");

        let isValid = true;
        isValid &= validateEmptyField("Ingrese su nombre", firstName);
        isValid &= validateEmptyField("Ingrese su apellido", lastName);
        isValid &= validateEmptyField("Ingrese el asunto", subject);
        isValid &= validateEmptyField("Ingrese su mensaje", message);
        isValid &= validateEmailFormat(email);

        if (isValid) {
            alert("Formulario enviado correctamente!");
            form.submit();
        }
    });
});