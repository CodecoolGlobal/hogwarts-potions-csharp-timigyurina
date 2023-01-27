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
import Card from '@mui/material/Card';

const PotionsTable = ({ potions, onDelete }) => {
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(false);
  const [deleteInProgress, setDeleteInProgress] = useState(false);
  const [potionToDelete, setPotionToDelete] = useState(null);

  const deletePotion = async (id) => {
    console.log(id);
    const url = await fetch(`https://localhost:44390/api/potions/${id}`, {
      method: "DELETE",
    });
    setIsLoading(true);

    try {
      const response = await fetch(url);

      setIsLoading(false);
      onDelete();

      if (!response.ok) {
        const error = response.message;
        setError(error);
        console.log(error);
        return;
      }
    } catch (err) {
      setIsLoading(false);
      setError(err.message);
      console.log(err);
    }
  };

  const openDeleteDialog = (potion) => {
    setDeleteInProgress(true);
    setPotionToDelete(potion);
  };

  const cancelDelete = () => {
    setDeleteInProgress(false);
    setPotionToDelete(null);
  };

  useEffect(() => {}, [potionToDelete]);

  return (
    <div className="potions">
      <Box sx={{ textAlign: "center" }}>
        <h2>Potions</h2>
      </Box>
      <Box sx={{ marginY: 1 }}>
        <Grid container direction="row" alignItems="center" spacing={2}>
          <Grid item xs={12} md={9}>
            <Button
              component={NavLink}
              variant="text"
              to="/potions/startbrewing"
            >
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
      {deleteInProgress && (
        <Card className="delete-potion-dialog" variant="outlined">
          Are you sure you want to delete {potionToDelete.name} (id:{" "}
          {potionToDelete.id})?
          <div className="delete-potion-dialog-buttons">
            <Button onClick={cancelDelete} variant="outlined" color="primary">
              Cancel
            </Button>
            <Button
              onClick={() => deletePotion(potionToDelete.id)}
              variant="contained"
              color="error"
            >
              Delete
            </Button>
          </div>
        </Card>
      )}
      {potions.length === 0 ? (
        "There are no Potions to show"
      ) : (
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
                  <TableCell align="center">
                    {potion.recipeId ? potion.recipeId : "none"}
                  </TableCell>
                  <TableCell align="center">{potion.studentId}</TableCell>
                  <TableCell align="center">
                    <Button
                      variant="text"
                      component={NavLink}
                      sx={{ width: "100%" }}
                      to={`${potion.id}/addingredient`}
                    >
                      <EditIcon />
                    </Button>
                  </TableCell>
                  <TableCell align="center">
                    <Button
                      variant="text"
                      onClick={() => openDeleteDialog(potion)}
                    >
                      <DisabledByDefaultIcon />
                    </Button>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      )}
    </div>
  );
};

export default PotionsTable;
