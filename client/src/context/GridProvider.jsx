import React, { Component } from 'react';
import GridContext from './GridContext';
import GridAPI from '../api/GridAPI';

export class GridProvider extends Component {
  state = {
    grids: [],
    currentGrid: null,
  };

  handleSetCurrentGrid = (grid) => {
    this.setState({ currentGrid: grid });
  };

  componentDidMount() {
    this.loadGrids();
  }

  async loadGrids() {
    let grids = await GridAPI.fetchGrids();
    console.log(grids)
    grids = grids.filter(grid => grid.establishmentDate != null && grid.establishmentDate !== '0001-01-01T00:00:00');
    this.setState({
      grids,
      currentGrid: grids[0]
    });
  }
  

  async fetchGridData(gridId) {
    const grid = await GridAPI.fetchGridById(gridId);  // Assuming GridAPI has this method.
    return grid;
  }
  
  handleCreateGrid = async (grid) => {
    const newGrid = await GridAPI.createGrid(grid);
    if (newGrid && newGrid.name) { 
      await this.loadGrids();
    }
  };

  handleUpdateGrid = async (gridId, updatedGrid) => {
    const data = await GridAPI.updateGrid(gridId, updatedGrid);
    if (data) {
      this.setState(prevState => {
        const updatedGrids = prevState.grids.map(grid => (grid.id === gridId ? data : grid));
        const updatedCurrentGrid = (prevState.currentGrid && prevState.currentGrid.id === gridId) ? data : prevState.currentGrid;
        return {
          grids: [...updatedGrids],
          currentGrid: { ...updatedCurrentGrid }
        };
      });      
    }
  };
  
  
  handleDeleteGrid = async gridId => {
    await GridAPI.deleteGrid(gridId);
    this.setState(prevState => ({
      grids: prevState.grids.filter(grid => grid.id !== gridId),
    }));
  };

  render() {
    const contextValue = {
      grids: this.state.grids,
      currentGrid: this.state.currentGrid,
      createGrid: this.handleCreateGrid,
      updateGrid: this.handleUpdateGrid,
      deleteGrid: this.handleDeleteGrid,
      setCurrentGrid: this.handleSetCurrentGrid,
      fetchGridData: this.fetchGridData
    };

    return (
      <GridContext.Provider value={contextValue}>
        {this.props.children}
      </GridContext.Provider>
    );
  }
}
  