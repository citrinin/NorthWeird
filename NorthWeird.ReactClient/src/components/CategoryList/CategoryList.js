import React, { Component } from 'react';
import Category from './components/Category';
import Spinner from '../Spinner/Spinner';

class CategoryList extends Component {
  state = {
    categories: [],
    isLoading: true
  }

  componentDidMount() {
    fetch('http://localhost:52370/api/categories/')
      .then(response => response.json())
      .then(data => {
        this.setState({ categories: data })
      })
      .finally(_ => this.setState({ isLoading: false }));
  }
  render() {
    return (
      <div className="CategoryList">
        {
          this.state.isLoading
            ? <Spinner />
            : <table className="table">
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
        }

      </div>
    );
  }
}

export default CategoryList;
