import { NavLink } from "react-router-dom";

import Box from "@mui/material/Box";
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

const RecipesTable = ({recipes}) => {
    return (
        <div className="Recipes">
          <Box sx={{ textAlign: "center" }}>
            <h2>Recipes</h2>
          </Box>

          <TableContainer component={Paper}>
            <Table sx={{ minWidth: 650 }} aria-label="simple table">
              <TableHead>
                <TableRow>
                  <TableCell align="center">Id</TableCell>
                  <TableCell align="center">Name</TableCell>
                  <TableCell align="center">StudentId</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {recipes.map((recipe) => (
                  <TableRow
                    key={recipe.id}
                    sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                  >
                    <TableCell align="center">{recipe.id}</TableCell>
                    <TableCell align="center">{recipe.name}</TableCell>
                    <TableCell align="center">{recipe.studentId}</TableCell>
                    <TableCell align="center">
                      <Button
                        variant="text"
                        component={NavLink}
                        sx={{ width: "100%" }}
                        to={`${recipe.id}/details`}
                      >
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

export default RecipesTable