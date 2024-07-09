import { Box, Button, Card, CardContent, TextField, Typography } from "@mui/material";
import Center from "./Center";
import useForm from "../hooks/useForm";
import { createApiEndpoint, ENDPOINTS } from "../api";

const getFreshModel=() =>({
    
    name:'',
    email:''
})


export default function Login(){

const{
    values,//store value
    setValues,
    errors,//for gettings errors
    setErrors,
    handleInputChange 
}=useForm(getFreshModel);
const login = e=> {
    e.preventDefault();
    if(validate())
   createApiEndpoint(ENDPOINTS.participant)
.post(values)
.then(res=>console.log(res))
.catch(err=>console.log(err))
}
//forvalidation Function.

const validate=()=>{
    let temp={}
    temp.email=(/\S+@\S+\.\S+/).test(values.email)?"":"Email Is Not Valid"
    temp.name=values.name!=""?"":"This field is required"
    setErrors(temp)
    return Object.values(temp).every(x=>x=="")
}
    return (
       <Center>
         <Card sx={{width:400}}>
            <CardContent  sx={{"textAlign":'Center'}}>
                <Typography variant="h3" sx={{'my':3}}>
                    Quiz APP
                </Typography>
            <Box sx={{
            '.MuiTextField-root':{
                margin:1,
                width:'90%'
            }
        }}>
        <form noValidate autoComplete="off" onSubmit={login}> 
            <TextField
            label="Name"
            name="name"
            value={values.name}
            onChange={handleInputChange}
            variant="outlined"
            {...(errors.name && {error:true,helperText:errors.name})}/>
            <TextField
            label="Eamil"
            name="email"
            value={values.email}
            onChange={handleInputChange}
            variant="outlined"
            {...(errors.email && {error:true,helperText:errors.email})}/> 

            <Button type="submit" variant="contained" size="large"
            sx={{width:'90%'}}>
                Start</Button>
        </form>
        </Box>
            </CardContent>
        </Card>
       </Center>
        
    )
}