import { NavLink } from "react-router-dom";
import Box from "@mui/material/Box";
import Grid from "@mui/material/Grid";
import TextField from "@mui/material/TextField";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";

const PageHeader = ({ title, buttonText, buttonLink }) => {
  return (
    <>
      <Box sx={{ textAlign: "center" }}>
        <Typography variant="h4" component="h2" marginY={2}>
          {title}
        </Typography>
      </Box>
      <Box sx={{ marginBottom: 2, marginX: 2, width: "95%" }}>
        <Grid
          container
          direction="row"
          justifyContent="space-around"
          spacing={2}
        >
          <Grid item xs={12} md={9}>
            <Button component={NavLink} variant="outlined" color="secondary" to={buttonLink}> 
              {buttonText}
            </Button>
          </Grid>
          <Grid item xs={12} md={3}>
            <TextField
              label="Filter"
              variant="outlined"
              size="small"
            />
          </Grid>
        </Grid>
      </Box>
    </>
  );
};

export default PageHeader;
