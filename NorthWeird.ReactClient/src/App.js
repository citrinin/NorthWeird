import React, { Component } from 'react';
import './App.css';
import ProductList from './components/ProductList/ProductList';

class App extends Component {
  render() {
    return (
      <div className="App">
        <header className="App-header">
          <ProductList/>
        </header>
      </div>
    );
  }
}

export default App;
