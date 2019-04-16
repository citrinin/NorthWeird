import React, { Component } from 'react';

class Category extends Component {
  render() {
    const { category: { categoryName, description } } = this.props;
    return (
      <tr>
        <td>
          {categoryName}
        </td>
        <td>
          {description}
        </td>
      </tr>
    );
  }
}

export default Category;
