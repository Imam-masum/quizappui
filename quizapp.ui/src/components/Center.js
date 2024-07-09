import { Container, Grid } from "@mui/material";

export default function Center(props){
   return(
    <Grid
    container
    direction="column"
    alignItems="center"
    justifyContent="center"
    sx={{"minHeight":'100vh'}}>
       <Grid item={1}>
{props.children}
       </Grid>
    </Grid>
   )
}