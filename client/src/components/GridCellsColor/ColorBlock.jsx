import React from 'react';
import { Paper } from '@material-ui/core';
import useStyles from './styles';

const ColorBlock = ({ cell, onClick }) => {
    const classes = useStyles();
    const gradientBackground = `linear-gradient(45deg, ${cell.colorstatus} 0%, ${cell.colorstatus} 55%, #fff 75%)`;

    return (
        <Paper 
            key={cell.id} 
            className={classes.gridItem} 
            elevation={3}
            onClick={onClick}
            style={{ 
                gridRow: cell.rowposition,
                gridColumn: cell.columnposition
            }}
        >
            <div className={classes.colorBlock} style={{ background: gradientBackground, borderBottomColor: cell.colorstatus }}></div>
        </Paper>
    );
};

export default ColorBlock;
