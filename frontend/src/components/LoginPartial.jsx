import React from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';

import {
    Nav
} from 'react-bootstrap';

import {
    Link
} from "react-router-dom";

function LoginPartial({ isAuthenticated, setIsAuthenticated }) {

    if(isAuthenticated) {
        return (
            <Nav className='navbar-nav me-auto mb-2 mb-lg-0'>
                <Nav.Link 
                    className='nav-link'
                    onClick={e => {
                        setIsAuthenticated(false);
                        sessionStorage.removeItem("user");
                        sessionStorage.removeItem("accessToken");
                        sessionStorage.removeItem("refreshToken");
                    }}
                >logout</Nav.Link>
            </Nav>
        )
    } else {
        return (
            <Nav className='navbar-nav me-auto mb-2 mb-lg-0'>
                <Link className='nav-link' to={"/login"}>Login</Link>
                <Link className='nav-link' to={"/register"}>Register</Link>
            </Nav>
        )
    }

    
}

export default LoginPartial