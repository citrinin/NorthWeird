import React, { Component } from 'react';
import Category from './components/Category';

class CategoryList extends Component {
  state = {
    categories: []
  }

  componentDidMount() {
    fetch('http://localhost:52370/api/categories/')
      .then(response => response.json())
      .then(data => {
        this.setState({ categories: data })
      });
  }
  render() {
    return (
      <div className="CategoryList">
        <table className="table">
          <thead>
            <tr>
              <th>Category name</th>
              <th>Category description</th>
            </tr>
          </thead>
          <tbody>
            {
              this.state.categories.map(category => <Category key={category.categoryId} category={category} />)
            }
          </tbody>
        </table>
      </div>
    );
  }
}

export default CategoryList;
