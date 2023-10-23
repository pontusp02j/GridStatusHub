import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';

const useStyles = makeStyles((theme) => ({
  controlsContainer: {
    display: 'flex',
    justifyContent: 'flex-end',
    marginTop: theme.spacing(2),
  },
  button: {
    marginLeft: theme.spacing(1),
    width: '100%'
  },
}));

const GridControlsComp = ({ onCreateNew, editingGridId }) => {
  const classes = useStyles();

  return (
    <div className={classes.controlsContainer}>
      <Button 
        variant="contained" 
        color="primary" 
        className={classes.button} 
        onClick={onCreateNew}
        disabled={editingGridId !== null}
      >
        Skapa ny
      </Button>
    </div>
  );
};


export default GridControlsComp;
