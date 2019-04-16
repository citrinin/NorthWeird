import React, { Component } from 'react';

class Product extends Component {
  render() {
    const { product: { productName, quantityPerUnit, category } } = this.props;
    return (
      <tr>
        <td>
          {productName}
        </td>
        <td>
          {category.categoryName}
        </td>
        <td>
          {quantityPerUnit}
        </td>
      </tr>
    );
  }
}

export default Product;
