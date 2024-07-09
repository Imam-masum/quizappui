import axios from "axios";
export const BaseUrl='http://localhost:5187/';
export const ENDPOINTS={
    participant:'participant'
}
export const createApiEndpoint=endpoint=>{

    let url= BaseUrl+'api/'+ endpoint +'/';
    return{
        //get all data
        fetch : ()=> axios.get(url),

        //get by id
        fetchById:id =>axios.get(url+id),

        //insert new record
        post:newRecord => axios.post(url, newRecord),

        //update existing record
        put:(id,updateRecord) => axios.put(url+id,updateRecord),

        //delete existing record
        delete:id=>axios.delete(url,id),

    }
}