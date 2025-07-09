
import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/auth_footer.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorSignInFooter extends StatelessWidget {
  const DoctorSignInFooter({
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: AuthFooter(
        text1: S.of(context).dont_have_account,
        text2: S.of(context).sign_up,
        onTap: (){
             context.push(AppRouter.doctorSignUpFirstView);
        },
      ),
    );
  }
}
