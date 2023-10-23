import './App.css';
import React from 'react';
import { CssBaseline, Container, Grid } from '@material-ui/core';
import GridProvider from './context/GridProvider';
import GridContainerComp from './components/GridContainer/index';

function App() {

  return (
    <GridProvider>
      <CssBaseline />
      <Container>
        <Grid container justifyContent="center">
          <Grid item xs={12} sm={10} md={8} lg={6}>
            <GridContainerComp />
          </Grid>
        </Grid>
      </Container>
    </GridProvider>
  );
}

export default App;
