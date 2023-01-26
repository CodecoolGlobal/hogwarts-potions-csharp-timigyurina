import { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";

import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Button from "@mui/material/Button";
import EditIcon from "@mui/icons-material/Edit";
import DisabledByDefaultIcon from "@mui/icons-material/DisabledByDefault";
import TextField from "@mui/material/TextField";

const Potions = () => {
  const [potions, setPotions] = useState([]);

  const fetchPotions = async () => {
    const url = "https://localhost:44390/api/potions";
    const response = await fetch(url);
    const data = await response.json();
    console.log(data);
    setPotions(data);
  };

  useEffect(() => {
    fetchPotions();
  }, []);

  return (
    <div className="Potions">
      <Box sx={{ textAlign: "center" }}>
        <h2>Potions</h2>
      </Box>
      <Box sx={{ marginY: 1 }}>
        <Grid container direction="row" alignItems="center" spacing={2}>
          <Grid item xs={12} md={9}>
            <Button component={NavLink}  variant="text" to="/startbrewing">
              Start a new Potion
            </Button>
          </Grid>
          <Grid item xs={12} md={3}>
            <TextField
              id="outlined-basic"
              label="Filter"
              variant="outlined"
              size="small"
            />
          </Grid>
        </Grid>
      </Box>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell align="center">Id</TableCell>
              <TableCell align="center">Name</TableCell>
              <TableCell align="center">Brewing status</TableCell>
              <TableCell align="center">RecipeId</TableCell>
              <TableCell align="center">StudentId</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {potions.map((potion) => (
              <TableRow
                key={potion.id}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell align="center">{potion.id}</TableCell>
                <TableCell align="center">{potion.name}</TableCell>
                <TableCell align="center">{potion.brewingStatus}</TableCell>
                <TableCell align="center">{potion.recipeId ? potion.recipeId : "none"}</TableCell>
                <TableCell align="center">{potion.studentId}</TableCell>
                <TableCell align="center">
                  <Button variant="text" component={NavLink} sx={{ width: '100%' }} to={`${potion.id}/addingredient`}>
                    <EditIcon />
                  </Button>
                </TableCell>
                <TableCell align="center">
                  <Button variant="text">
                    <DisabledByDefaultIcon />
                  </Button>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
    </div>
  );
};

export default Potions;