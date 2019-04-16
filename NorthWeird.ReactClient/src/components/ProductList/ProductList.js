import React, { Component } from 'react';
import Product from './components/Product';
import Spinner from '../Spinner/Spinner';

class ProductList extends Component {
  state = {
    products: [],
    isLoading: true
  }

  componentDidMount() {
    fetch('http://localhost:52370/api/products/')
      .then(response => response.json())
      .then(data => {
        this.setState({ products: data })
      })
      .finally(_ => this.setState({ isLoading: false }));
  }
  render() {
    return (
      <div className="ProductList">
        {
          this.state.isLoading
            ? <Spinner />
            : <table className="table">
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
        }

      </div>
    );
  }
}

export default ProductList;
