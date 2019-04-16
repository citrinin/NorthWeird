import React, { Component } from 'react';
import Product from './components/Product';

var myInit = {
  method: 'GET'
};

class ProductList extends Component {
  state = {
    products: []
  }

  componentDidMount() {
    fetch('http://localhost:52370/api/products/', myInit)
      .then(response => response.json())
      .then(data => {
        console.log(data);
        this.setState({ products: data })
      });
  }
  render() {
    return (
      <div className="ProductList">
        <table className="table">
          <thead>
            <tr>
              <th>Product name</th>
              <th>Category</th>
              <th>Quantity per unit</th>
            </tr>
          </thead>
          <tbody>
            {
              this.state.products.map(product => <Product key={product.productId} product={product} />)
            }
          </tbody>
        </table>
      </div>
    );
  }
}

export default ProductList;
