import React, { useState } from 'react';
import { Navigate, redirect } from 'react-router-dom';

import 'bootstrap/dist/css/bootstrap.min.css';

import {
    Form,
    Button,
    Navbar,
    Nav,
    Container
} from 'react-bootstrap';

function Login({ isAuthenticated, setIsAuthenticated }) {

    const [data, setData] = useState({ name: "", password: "" });

    async function Login(e) {
        e.preventDefault();

        if (data.name.trim() != "" && data.password.trim() != "") {

            let token = {};
            const response = await fetch("https://localhost:9001/api/account/authenticate", {
                method: 'post',
                headers: {
                    "Accept": "application/json",
                    "Content-type": "application/json"
                },
                body: JSON.stringify({ id: "", name: data.name, password: data.password })
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

    if(isAuthenticated) {
        return (
            <h1 className='text-center'>Вы авторизировались</h1>
        )
    } else {
        return (
            <Container className='text-center' style={{ width: "300px" }}>
                <h1>Log In</h1>
                <form className='mt-3'>
                    <Form.Group className='mb-3'>
                        <Form.Control
                            type='text'
                            value={data.name}
                            placeholder='login'
                            onChange={e => setData({ ...data, name: e.target.value })}
                        />
                    </Form.Group>
    
                    <Form.Group className='mb-3'>
                        <Form.Control
                            value={data.password}
                            placeholder='password'
                            type='password'
                            onChange={e => setData({ ...data, password: e.target.value })}
                        />
                    </Form.Group>
    
    
                    <Button
                        variant='outline-dark'
                        onClick={Login}
                    >Log in</Button>
                </form>
            </Container>
        )
    }    
}

export default Login