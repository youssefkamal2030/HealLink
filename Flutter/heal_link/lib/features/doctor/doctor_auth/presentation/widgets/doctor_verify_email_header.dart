
import 'package:flutter/cupertino.dart';
import 'package:heal_link/core/widgets/custom_auth_header.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorVerifyEmailHeader extends StatelessWidget {
  const DoctorVerifyEmailHeader({super.key});

  @override
  Widget build(BuildContext context) {
    return SliverToBoxAdapter(
      child: CustomAuthHeader(headerTitle: S.of(context).verify_email),
    );
  }
}

