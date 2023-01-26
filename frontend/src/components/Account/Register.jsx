import React, { useState } from 'react';
import { Navigate, redirect } from "react-router-dom";

import 'bootstrap/dist/css/bootstrap.min.css';

import {
    Form,
    Button,
    Container
} from 'react-bootstrap';

function Register({ isAuthenticated, setIsAuthenticated }) {

    const [data, setData] = useState({ name: "", password: "", repeatPassword: "" });

    async function Register() {
        if (data.name.trim() != "" && data.password.trim() != "" && data.password == data.repeatPassword) {

            let token = {};
            const response = await fetch("https://localhost:9001/api/account/register", {
                method: 'post',
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify({ name: data.name, password: data.password, confirmPassword: data.repeatPassword })
            });

            if (response.ok) {
                const token = await response.json();

                sessionStorage.setItem("accessToken", token.accessToken);
                sessionStorage.setItem("refreshToken", token.refreshToken);
                sessionStorage.setItem("user", data.name);

                setIsAuthenticated(true);

                setData({ name: "", password: "" });
            } else {
                alert(response.title)
            }
        } else {
            alert("Заполните данные")
        }
    }

    if (isAuthenticated) {
        return (
            <h1 className='text-center'>Вы авторизировались</h1>
        )
    } else {
        return (
            <Container className='text-center' style={{ width: "300px" }}>
                <h1>Register</h1>
                <form className='mt-4'>
                    <Form.Group className='m-3'>
                        <Form.Control
                            type='text'
                            value={data.name}
                            placeholder='login'
                            onChange={e => setData({ ...data, name: e.target.value })}
                        />
                    </Form.Group>

                    <Form.Group className='m-3'>
                        <Form.Control
                            value={data.password}
                            placeholder='password'
                            type='password'
                            onChange={e => setData({ ...data, password: e.target.value })}
                        />
                    </Form.Group>

                    <Form.Group className='m-3'>
                        <Form.Control
                            value={data.repeatPassword}
                            placeholder='repeat password'
                            type='password'
                            onChange={e => setData({ ...data, repeatPassword: e.target.value })}
                        />
                    </Form.Group>


                    <Button
                        className='mt-1'
                        variant='outline-dark'
                        onClick={Register}
                    >Register</Button>
                </form>
            </Container>
        )
    }
}

export default Register