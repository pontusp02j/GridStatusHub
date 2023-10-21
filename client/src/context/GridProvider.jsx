import React, { Component } from 'react';
import GridContext from './GridContext';
import GridAPI from '../api/GridAPI';
import { validateGridName, sValidMessage } from './../utili/GridValidations';


export class GridProvider extends Component {
  state = {
    grids: [],
    currentGrid: null,
    activeGridId: null,
    
  };

  setActiveGridId = (gridId) => {
    this.setState({ activeGridId: gridId });
  };

  handleSetCurrentGrid = (grid) => {
    this.setState({ currentGrid: grid });
  };

  componentDidMount() {
    this.loadGrids();
  }

  async loadGrids() {
    let grids = await GridAPI.fetchGrids();
    grids = grids.filter(grid => grid.establishmentDate != null && grid.establishmentDate !== '0001-01-01T00:00:00');
    this.setState({
      grids,
      currentGrid: grids[0]
    });
  }
  
  async fetchGridData(gridId) {
    const grid = await GridAPI.fetchGridById(gridId);
    return grid;
  }
  
  handleCreateGrid = async (grid) => {

    const error = validateGridName(grid.name, this.state.grids);
    if (error) {
      window.alert(error);
      return;
    }

    const newGrid = await GridAPI.createGrid(sValidMessage, grid);
    if (newGrid && newGrid.name) 
    { 
      await this.loadGrids();
      this.setActiveGridId(newGrid.id);
      return newGrid;
    }
  };

  handleUpdateGrid = async (gridId, updatedGrid) => {

    const error = validateGridName(updatedGrid.name, this.state.grids);
    if (error) {
      window.alert(error);
      return;
    }

    const data = await GridAPI.updateGrid(sValidMessage, updatedGrid);
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
      fetchGridData: this.fetchGridData,
      setActiveGridId: this.setActiveGridId
    };

    return (
      <GridContext.Provider value={contextValue}>
        {this.props.children}
      </GridContext.Provider>
    );
  }
}
  