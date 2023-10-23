import { makeStyles } from '@material-ui/core';

const useStyles = makeStyles((theme) => ({
    activeRow: {
        backgroundColor: theme.palette.action.selected,
    },
    tableContainer: {
        marginTop: theme.spacing(10),
        marginBottom: theme.spacing(10),
        maxWidth: 650,
        margin: '0 auto',
        border: '1px solid rgba(128, 128, 128, 0.3)'
    },
    tableRow: {
        height: '100px',
        cursor: 'pointer',
        '&.editing': {
            cursor: 'default',
        },
        '&:not(.editing):hover': {
            backgroundColor: theme.palette.action.hover,
        }
    },
}));

export default useStyles;