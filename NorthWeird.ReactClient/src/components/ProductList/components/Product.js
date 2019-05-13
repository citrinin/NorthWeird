import React, { Component } from 'react';

class Product extends Component {
  render() {
    const { product: { productName, quantityPerUnit, categoryName } } = this.props;
    return (
      <tr>
        <td>
          {productName}
        </td>
        <td>
          {categoryName}
        </td>
        <td>
          {quantityPerUnit}
        </td>
      </tr>
    );
  }
}

export default Product;
