
import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_row_with_divider.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignUpRowDivider extends StatelessWidget {
  const DoctorSignUpRowDivider({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: AuthRowWithDivider(
        text: S.of(context).or_sign_up_with,
      ),
    );
  }
}
