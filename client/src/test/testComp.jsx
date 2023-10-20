import React, { Component } from 'react';
import GridContext from './../context/GridContext';

class YourComponent extends Component {
  static contextType = GridContext;

  render() {
    const { grids, currentGrid, createGrid, updateGrid, deleteGrid } = this.context;

    // Use these values/functions in your component
    return (
      <div>
        {/* display grids or use any of the functions */}
      </div>
    );
  }
}

export default YourComponent;
