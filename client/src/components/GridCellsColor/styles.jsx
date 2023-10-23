import { makeStyles } from '@material-ui/core/styles';

const useStyles = makeStyles((theme) => ({
    title: {
        textAlign: 'center',
        margin: `${theme.spacing(2)}px 0`,
        fontSize: '1.5em',
    },
    gridContainer: {
        display: 'grid',
        gridTemplateColumns: 'repeat(5, 1fr)',
        gridTemplateRows: 'repeat(5, 1fr)',
        gap: theme.spacing(1),
        margin: `${theme.spacing(12)}px auto`,
        boxShadow: '0px 0px 10px rgba(0, 0, 0, 0.5)',
        padding: theme.spacing(1),
    },
    gridItem: {
        width: '100%',
        position: 'relative',
        border: '1px solid #aaa',
        boxShadow: '0px 5px 15px rgba(0, 0, 0, 0.2)',
        marginBottom: theme.spacing(1),
        transition: 'transform 0.3s ease, box-shadow 0.3s ease',
        cursor: 'pointer',
        '&:hover': {
            transform: 'scale(1.05)',
            boxShadow: '0px 8px 20px rgba(0, 0, 0, 0.3)',
        }
    },
    colorBlock: {
        width: '100%', 
        height: '8em', 
        borderBottom: '5px solid',
        background: (props) => `linear-gradient(45deg, ${props.color} 0%, ${props.color} 55%, #fff 75%)`,
    },
    errorBox: {
        position: 'absolute',
        left: '0%',
        top: '5em',
        width: '100%',
        padding: '2em',
        textAlign: 'center',
        borderRadius: theme.shape.borderRadius * 1,
    },
}));

export default useStyles;
