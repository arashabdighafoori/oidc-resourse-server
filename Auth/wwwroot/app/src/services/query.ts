export async function send(path: string, body: string) {
  try {
    const headers = {
      "Content-Type": "application/json",
      "X-Requested-With": "Graphql",
      Accept: "application/json",
    };

    const response = await fetch(path, {
      method: "POST",
      headers: headers,
      body: body,
    });
    if (response.ok) {
      return response.json();
    } else {
      console.log(response.status);
    }
  } catch (error) {
    // return { error };
  }
}

export async function query(query: string, args: unknown) {
  // console.log(
  //   JSON.stringify({
  //     query: query,
  //     variables: args,
  //   })
  // );
  var result = await this.send(
    "/graphql",
    JSON.stringify({
      query: query,
      variables: args,
    })
  );
  return result;
}
