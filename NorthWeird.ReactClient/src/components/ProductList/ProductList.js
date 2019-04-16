import React, { Component } from 'react';
import Product from './components/Product';

let isLoading = false;
class ProductList extends Component {
  state = {
    products: []
  }

  componentDidMount() {
    isLoading = true;
    fetch('http://localhost:52370/api/products/')
      .then(response => response.json())
      .then(data => {
        this.setState({ products: data })
      })
      .finally(_ => isLoading = false);
  }
  render() {
    return (
      <div className="ProductList">
        {
          isLoading
            ? <table className="table">
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
            : <img src="https://i0.wp.com/media.boingboing.net/wp-content/uploads/2015/10/tumblr_nlohpxGdBi1tlivlxo1_12801.gif?w=500" alt="spinner" />
        }

      </div>
    );
  }
}

export default ProductList;
