import { NavLink } from "react-router-dom";

import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import Button from '@mui/material/Button';
import Box from "@mui/material/Box";

const Navlinks = () => {
  return (
   
    <List className='navbar' sx={{ width: '100%',  bgcolor: 'background.paper' }}>
        <ListItem sx={{justifyContent:"center"}} >
            <Button variant="contained" component={NavLink} sx={{ width: '70%' }} to="/">Home</Button>
        </ListItem >
        <ListItem sx={{ justifyContent: "center" }}>
            <Button variant="contained" component={NavLink} sx={{ width: '70%' }} to="/potions">Potions</Button>
        </ListItem >
        <ListItem sx={{justifyContent:"center"}}>
            <Button variant="contained" component={NavLink} sx={{ width: '70%' }} to="/potions/startbrewing">Start a new Potion</Button>
        </ListItem >
        {/* <ListItem sx={{justifyContent:"center"}}>
            <Button variant="contained" component={NavLink} sx={{ width: '100%' }} to="/addingredient">Add ingredient to Potion</Button>
        </ListItem > */}
    </List >

  )
}

export default Navlinks