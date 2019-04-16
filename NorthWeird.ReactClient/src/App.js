import React, { Component } from 'react';
import './App.css';
import { BrowserRouter as Router, Route, Link } from "react-router-dom";
import ProductList from './components/ProductList/ProductList';
import Home from './components/Home/Home';
import CategoryList from './components/CategoryList/CategoryList';

class App extends Component {
  render() {
    return (
      <div className="App">
        <Router>
          <div>
            <nav className="navbar navbar-expand-lg navbar-light bg-light">
              <a className="navbar-brand" href="/">NorthWeird</a>
              <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                <span className="navbar-toggler-icon"></span>
              </button>

              <div className="collapse navbar-collapse" id="navbarSupportedContent">
                <ul className="navbar-nav mr-auto">
                  <li className="nav-item active">
                    <Link className="nav-link" to="/">Home</Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/products">Products</Link>
                  </li>
                  <li className="nav-item">
                    <Link className="nav-link" to="/categories">Categories</Link>
                  </li>
                </ul>
              </div>
            </nav>

            <Route exact path="/" component={Home} />
            <Route path="/products" component={ProductList} />
            <Route path="/categories" component={CategoryList} />
          </div>
        </Router>
      </div>
    );
  }
}

export default App;
