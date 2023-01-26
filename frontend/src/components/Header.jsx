import React from 'react'

import {
    BrowserRouter as Router,
    Route,
    Routes,
    Link
} from "react-router-dom";

import 'bootstrap/dist/css/bootstrap.min.css';

import {
    Button,
    Navbar,
    Nav,
    Container
} from 'react-bootstrap';

import LoginPartial from './LoginPartial';
import Home from './Home';
import Login from './Account/Login';
import Register from './Account/Register';

function Header({ isAuthenticated, setIsAuthenticated }) {
    return (
        <Router>
            <div>
                <Navbar bg="dark" variant="dark" className='navbar-expand-sm navbar-toggleable-sm border-bottom box-shadow mb-3'>
                    <Container>
                        <Nav className='navbar-collapse collapse d-sm-inline-flex'>
                            <ul className='navbar-nav flex-grow-1'>
                                <li><Link className='nav-link' to={"/"}>Home</Link></li>
                            </ul>
                        </Nav>

                        <LoginPartial isAuthenticated={isAuthenticated} setIsAuthenticated={setIsAuthenticated} />
                    </Container>
                </Navbar>
                <Routes>
                    <Route path={'/'} element={<Home isAuthenticated={isAuthenticated} />} />
                    <Route path={'/login'} element={<Login isAuthenticated={isAuthenticated} setIsAuthenticated={setIsAuthenticated} />} />
                    <Route path={'/register'} element={<Register isAuthenticated={isAuthenticated} setIsAuthenticated={setIsAuthenticated} />} />
                </Routes>
            </div>
        </Router>
    )
}

export default Header