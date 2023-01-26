import { React, useState, useEffect } from "react";
import Header from "./components/Header";

function App() {

  const [isLoaded, setIsLoaded] = useState(false);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {

    const token = {};
    token.accessToken = sessionStorage.getItem("accessToken");
    token.refreshToken = sessionStorage.getItem("refreshToken");

    if (token.accessToken != null && token.refreshToken != null) {

      setIsAuthenticated(true);
    }

    setIsLoaded(true);
  }, []);

  if (!isLoaded) {
    return (
      <div>Loading...</div>
    )
  } else {
    return (
      <Header isAuthenticated={isAuthenticated} setIsAuthenticated={setIsAuthenticated}></Header>
    )
  }
}

export default App;
