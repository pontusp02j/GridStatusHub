import React from 'react';
import { Paper } from '@material-ui/core';
import useStyles from './styles';

const ErrorMessage = () => {
    const classes = useStyles();
    return (
        <Paper className={classes.errorBox} elevation={3}>
            <p>Klicka på en av namnen för att få upp rutnätet, det går även att byta rutnät mellan de.</p>
        </Paper>
    );
};

export default ErrorMessage;
