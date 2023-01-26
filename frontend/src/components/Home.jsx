import React from 'react'

import 'bootstrap/dist/css/bootstrap.min.css';

import {
    Button,
    Navbar,
    Nav,
    Container,
    Stack,
    Image
} from 'react-bootstrap';

function Home({ isAuthenticated }) {

    const showAccessToken = () => {
        // let elem = document.querySelector(".access-token");
        // elem.innerHTML = sessionStorage.getItem("accessToken");
        alert(sessionStorage.getItem("accessToken"));
    };

    const showRefreshToken = () => {
        //let elem = document.querySelector(".refresh-token");
        //elem.innerHTML = sessionStorage.getItem("refreshToken");
        alert(sessionStorage.getItem("refreshToken"));
    };

    if (!isAuthenticated) {
        return (
            <Container>
                <h1 className='text-center'>Авторизируйтесь</h1>
            </Container>
        )
    } else {
        return (
            <Container className='text-center'>
                <div className='d-flex justify-content-center'>
                    <div className='p-4 m-4 card'
                        style={{ "width": "350px" }}>
                        <div>
                            <i className="fa-solid fa-user" style={{ fontSize: "180px" }}></i>
                        </div>

                        <div>
                            <div className='d-flex justify-content-between m-2 mb-3 mt-4'>
                                <div>username: </div>
                                <div className='access-token'>
                                    <strong>{sessionStorage.getItem("user")}</strong>
                                </div>
                            </div>
                            <div className='d-flex justify-content-between m-2'>
                                <div>access token: </div>
                                <div className='access-token'>
                                    <Button onClick={showAccessToken}>show</Button>
                                </div>
                            </div>
                            <div className='d-flex justify-content-between m-2'>
                                <div>refresh token: </div>
                                <div className='refresh-token'>
                                    <Button onClick={showRefreshToken}>show</Button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </Container>
        )
    }
}

export default Home