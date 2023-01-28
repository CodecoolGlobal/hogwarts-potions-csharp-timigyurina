import { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";

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
import PotionDelete from "./PotionDelete";
import PageHeader from "../Shared/PageHeader";

const PotionsTable = ({ potions, onDelete, onDeletionStart, onDeletionEnd, onDeletionError }) => {
  const [deleteInProgress, setDeleteInProgress] = useState(false);
  const [potionToDelete, setPotionToDelete] = useState(null);

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
      <PageHeader
        title="Potions"
        buttonText="Start a new Potion"
        buttonLink="/potions/startbrewing"
      />

      {deleteInProgress && (
        <PotionDelete
          potionToDelete={potionToDelete}
          onDelete={onDelete}
          onCancelDelete={cancelDelete}
          onDeletionStart={onDeletionStart}
          onDeletionEnd={onDeletionEnd}
          onDeletionError={onDeletionError}
        />
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
                      to={`/potions/${potion.id}/addingredient`}
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
