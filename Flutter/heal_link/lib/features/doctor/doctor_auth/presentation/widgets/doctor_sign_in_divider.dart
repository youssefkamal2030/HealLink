import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_row_with_divider.dart';

class DoctorSignInRowDivider extends StatelessWidget {
  const DoctorSignInRowDivider({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(child: AuthRowWithDivider(text: "or Sign in with "));
  }
}
