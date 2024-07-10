import { createContext, useState } from "react"

export const stateContext =createContext();

const getFreshContext=()=>{
    return{
        particiantId:0,
        timeTaken:0,
        selectedOptions:[]
    }
}
export  function ContextProvider({children}){
     const [context,setContext]=useState(getFreshContext());
    return(
      <stateContext.Provider value={context.setContext}> 
        {children}
      </stateContext.Provider>
    )
}