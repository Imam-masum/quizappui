import { useContext } from "react"
import { stateContext } from "../hooks/useStateContext"

export default function Ouestion(){
    const {context,setContext}=useContext(stateContext);
    setContext({
        ...context,
        timeTaken:1
    })
    return(
        <div></div>
    )
}