function autoSizeAll(gridApi, skipHeader) {
    const allColumnIds = [];
    gridApi.getColumns().forEach((column) => {
        allColumnIds.push(column.getId());
    });

    gridApi.autoSizeColumns(allColumnIds, skipHeader);
}


