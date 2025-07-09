
import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/auth_footer.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignUpFooter extends StatelessWidget {
  const DoctorSignUpFooter({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: AuthFooter(
        text1: S.of(context).have_account,
        text2: S.of(context).sign_in,
        onTap: (){},
      ),
    );
  }
}
