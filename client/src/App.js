import './App.css';
import React from 'react';
import { CssBaseline, Container, Grid } from '@material-ui/core';
import { GridProvider } from './context/GridProvider';
import GridContainerComp from './components/GridsContainerComp';

if ('serviceWorker' in navigator) {
  window.addEventListener('load', function() {
      navigator.serviceWorker.register('/serviceWorker.js').then(function(registration) {
          console.log('ServiceWorker registration successful with scope: ', registration.scope);
      }, function(err) {
          console.log('ServiceWorker registration failed: ', err);
      });
  }); 
}

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
